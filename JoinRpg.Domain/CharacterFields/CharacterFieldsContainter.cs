using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JoinRpg.DataModel;
using Newtonsoft.Json;

// ReSharper disable once CheckNamespace
namespace JoinRpg.Domain
{
  public class CharacterFieldsContainter : IReadOnlyDictionary<int, CharacterFieldValue>
  {
    private Character Character { get; }

    private readonly IReadOnlyDictionary<int, CharacterFieldValue> _dictionary;

    public CharacterFieldsContainter(Character character)
    {
      Character = character;
      _dictionary = LoadDictionaryFromCharacter(character);
    }

    private Dictionary<int, CharacterFieldValue> LoadDictionaryFromCharacter(Character character)
    {
      var dictionary = LoadFieldsFromJsonString(character);
      var absentFields = character.Project.ActiveProjectFields.Where(f => !dictionary.ContainsKey(f.ProjectCharacterFieldId));
      foreach (var field in absentFields)
      {
        dictionary.Add(field.ProjectCharacterFieldId, new CharacterFieldValue(this, field, null));
      }
      return dictionary;
    }

    private Dictionary<int, CharacterFieldValue> LoadFieldsFromJsonString(Character character)
    {
      return JsonConvert.DeserializeObject<Dictionary<int, string>>(character.JsonData)
        .ToDictionary(obj => obj.Key, obj => new CharacterFieldValue(this, FindField(obj.Key), obj.Value));
    }

    private ProjectCharacterField FindField(int key)
    {
      return Character.Project.AllProjectFields.Single(field => field.ProjectCharacterFieldId == key);
    }

    public void Update()
    {
      Character.JsonData = JsonConvert.SerializeObject(_dictionary);
    }

    IEnumerator<KeyValuePair<int, CharacterFieldValue>> IEnumerable<KeyValuePair<int, CharacterFieldValue>>.GetEnumerator() => _dictionary.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _dictionary).GetEnumerator();
    int IReadOnlyCollection<KeyValuePair<int, CharacterFieldValue>>.Count => _dictionary.Count;
    bool IReadOnlyDictionary<int, CharacterFieldValue>.ContainsKey(int key) => _dictionary.ContainsKey(key);
    bool IReadOnlyDictionary<int, CharacterFieldValue>.TryGetValue(int key, out CharacterFieldValue value) => _dictionary.TryGetValue(key, out value);
    CharacterFieldValue IReadOnlyDictionary<int, CharacterFieldValue>.this[int key] => _dictionary[key];
    IEnumerable<int> IReadOnlyDictionary<int, CharacterFieldValue>.Keys => _dictionary.Keys;
    IEnumerable<CharacterFieldValue> IReadOnlyDictionary<int, CharacterFieldValue>.Values => _dictionary.Values;
  }

  public static class CharacterFieldsExtractor
  {
    public static CharacterFieldsContainter Fields(this Character character) => new CharacterFieldsContainter(character);
  }
}