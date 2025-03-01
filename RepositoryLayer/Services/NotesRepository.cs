using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NotesRepository : INotesRepository
    {
        private readonly AppDbContext _context;
        private readonly RedisCacheService _redisCache;
        private const string CachePrefix = "UserNotes_";

        public NotesRepository(AppDbContext context, RedisCacheService redisCache)
        {
            _context = context;
            _redisCache = redisCache;
        }

        public Notes CreateNote(Notes note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();

            // 🔹 Invalidate cache for the user
            _redisCache.Remove($"{CachePrefix}{note.Id}");

            return note;
        }

        public List<Notes> GetUserNotes(int userId)
        {
            string cacheKey = $"{CachePrefix}{userId}";
            var cachedNotes = _redisCache.Get<List<Notes>>(cacheKey);

            if (cachedNotes != null)
            {
                return cachedNotes;
            }

            var notes = _context.Notes.Where(n => n.Id == userId).ToList();
            _redisCache.Set(cacheKey, notes, TimeSpan.FromMinutes(5)); // Cache for 5 minutes

            return notes;
        }

        public Notes GetNotesById(int noteId, int userId)
        {
            string cacheKey = $"Note_{noteId}_{userId}";
            var cachedNote = _redisCache.Get<Notes>(cacheKey);

            if (cachedNote != null)
            {
                return cachedNote;
            }

            Notes note = _context.Notes.FirstOrDefault(x => x.Notes_id == noteId && x.Id == userId);
            if (note == null)
            {
                throw new Exception("Note not found");
            }

            _redisCache.Set(cacheKey, note, TimeSpan.FromMinutes(5)); // Cache for 5 minutes
            return note;
        }

        public Notes UpdateNote(int userId, Notes note)
        {
            var existingNote = _context.Notes.FirstOrDefault(n => n.Notes_id == note.Notes_id && n.Id == userId);
            if (existingNote == null)
            {
                throw new Exception("Note not found");
            }

            // 🔹 Update note details
            existingNote.Title = note.Title;
            existingNote.Description = note.Description;
            existingNote.Color = note.Color;

            _context.Notes.Update(existingNote);
            _context.SaveChanges();

            // 🔹 Invalidate cache for the user and specific note
            _redisCache.Remove($"{CachePrefix}{userId}");
            _redisCache.Remove($"Note_{note.Notes_id}_{userId}");

            return existingNote;
        }

        public bool DeleteNote(int noteId, int userId)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Notes_id == noteId && n.Id == userId);
            if (note == null) return false;

            _context.Notes.Remove(note);
            _context.SaveChanges();

            // 🔹 Invalidate cache
            _redisCache.Remove($"{CachePrefix}{userId}");
            _redisCache.Remove($"Note_{noteId}_{userId}");

            return true;
        }

        public bool checkDeletedNote(int noteId, int userId)
        {
            var note = _context.Notes.FirstOrDefault(x => x.Notes_id == noteId && x.Id == userId);
            if (note == null)
            {
                return false;
            }

            note.IsDeleted = !note.IsDeleted;
            _context.SaveChanges();

            // 🔹 Invalidate cache
            _redisCache.Remove($"{CachePrefix}{userId}");
            _redisCache.Remove($"Note_{noteId}_{userId}");

            return true;
        }

        public bool checkArchieved(int noteId, int userId)
        {
            var note = _context.Notes.FirstOrDefault(x => x.Notes_id == noteId && x.Id == userId);
            if (note == null)
            {
                return false;
            }

            note.isArchive = !note.isArchive;
            _context.SaveChanges();

            // 🔹 Invalidate cache
            _redisCache.Remove($"{CachePrefix}{userId}");
            _redisCache.Remove($"Note_{noteId}_{userId}");

            return true;
        }
    }
}
