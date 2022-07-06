using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrancisForte {

    [CreateAssetMenu(fileName = "Album Object", menuName = "Music Manager/Create album", order = 3)]

    public class Album : ScriptableObject
    {
        [SerializeField]
        private string albumName;

        [SerializeField]
        private RenderTexture albumImage;

        [SerializeField]
        private List<Song> songList;

        public string GetAlbumName()
        {
            return albumName;
        }

        public RenderTexture GetAlbumImage()
        {
            return albumImage;
        }

        public List<Song> GetSongList()
        {
            return songList;
        }

    }

}