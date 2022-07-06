using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrancisForte {

    [System.Serializable]
    public class SongFile {

        public Object file;
        public Utils.SongDifficulty level;

    }

    [CreateAssetMenu(fileName = "Song Object", menuName = "Music Manager/Create song", order = 2)]

    public class Song : ScriptableObject
    {
        [SerializeField]
        private List<SongFile> songFiles;

        [SerializeField]
        private string songName;

        [SerializeField]
        private RenderTexture songImage;

        [SerializeField]
        private string version;

        

        [SerializeField]
        private List<string> authors;

        public string GetSongName()
        {
            return songName;
        }

        public RenderTexture GetSongImage()
        {
            return songImage;
        }

        public List<SongFile> GetSongFiles()
        {
            return songFiles;
        }

        public void AddAuthor(string author){
            this.authors.Add(author);
        }

        public List<string> GetAuthors()
        {
            return authors;
        }

        public void SetVersion(string version)
        {
            this.version = version;
        }

        public string GetVersion()
        {
            return version;
        }

    }

}