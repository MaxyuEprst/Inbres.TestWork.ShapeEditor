using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Editor.Entities.Shape.Models;

namespace Editor.Features.Saving
{
    public class FileShapeStorage
    {
        private readonly ShapeSerializer _serializer;
        private readonly string _filePath;

        public FileShapeStorage(ShapeSerializer serializer, string fileName = "editor-shapes.json")
        {
            _serializer = serializer;
            _filePath = GetFilePath(fileName);
            EnsureDirectoryExists();
        }

        public async Task SaveShapesAsync(IEnumerable<EditorShape> shapes)
        {
            try
            {
                var json = _serializer.Serialize(shapes);
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving shapes: {ex.Message}");
                throw;
            }
        }

        public async Task<List<EditorShape>> LoadShapesAsync()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<EditorShape>();

                var json = await File.ReadAllTextAsync(_filePath);
                return _serializer.Deserialize(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading shapes: {ex.Message}");
                return new List<EditorShape>();
            }
        }

        private string GetFilePath(string fileName)
            => Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "ShapeEditor",
                fileName
            );

        private void EnsureDirectoryExists()
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory!);
        }
    }
}