using Backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Backend.Services;

public class MongoDBService
{
    private readonly IMongoCollection<Student> _studentCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _studentCollection = database.GetCollection<Student>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CreateStudent(Student student)
    {
        await _studentCollection.InsertOneAsync(student);
        return;
    }

    public async Task<List<Student>> GetStudents()
    {
        return await _studentCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateStudent(string id, Student newStudent)
    {
        await _studentCollection.ReplaceOneAsync(student => student.Id == id, newStudent);
    }

    public async Task DeleteStudent(string id)
    {
        FilterDefinition<Student> filter = Builders<Student>.Filter.Eq("Id", id);
        await _studentCollection.DeleteOneAsync(filter);
        return;
    }
}