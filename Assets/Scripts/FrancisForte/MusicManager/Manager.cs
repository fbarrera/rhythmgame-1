using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Standards;


namespace FrancisForte {
    
    public class Manager : MonoBehaviour
    {

        [SerializeField]
        public List<Album> albumList;

        private MidiFile midiFile;

        // Start is called before the first frame update
        void Start()
        {
            
            foreach(Album album in albumList){
                Debug.Log("Album: " + album.GetAlbumName());
                int validSongs = album.GetSongList().Count;
                foreach(Song song in album.GetSongList()){
                    Debug.Log("# Track: " + song.GetSongName());
                    int validFiles = song.GetSongFiles().Count;
                    foreach(SongFile songFile in song.GetSongFiles()){
                        Debug.Log("## File: " + songFile.file);
                        Debug.Log("## Level: " + songFile.level);
                        string filePath = AssetDatabase.GetAssetPath(songFile.file);
                        string fileDirectory = Path.GetDirectoryName(filePath);
                        string fileExtension = Path.GetExtension(filePath);
                        if(fileExtension != ".mid"){
                            Debug.Log("## File is not a midi file");
                            validFiles--;
                        }
                        else{
                            // Set POO file to load and treat MIDI file
                        }
                        
                    }
                    if(validFiles == 0){
                        Debug.Log("## No valid files");
                        validSongs--;
                    }
                }
                if(validSongs == 0){
                    Debug.Log("# No valid songs");
                }
            }
        }
    }

}