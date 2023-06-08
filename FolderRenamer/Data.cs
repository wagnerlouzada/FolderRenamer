using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace FolderRenamer
{

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Catalog
    {
        public List<CatalogItem> CatalogData { get; set; }
        public Catalog()
        {
            CatalogData = new List<CatalogItem>();
        }
    }

    public enum ItemType
    {
        none,
        Volume,
        Folder,
        File
    }

    public enum ItemStatus
    {
        none,
        Ok,
        NOk,
        Empty
    }

    public enum searchItemCategory
    {
        none,
        letter,
        word,
        preposition
    }

    public class SearchParm
    {
        public String Word { get; set; }
        public String Soundex { get; set; }
        public searchItemCategory Type { get; set; }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CatalogItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int? FatherId { get; set; }

        public ItemType Type { get; set; }

        [Indexed, SQLite.MaxLength(1000)]
        public String Name { get; set; }

        [Indexed, SQLite.MaxLength(200)]
        public String Title { get; set; }

        [Indexed, SQLite.MaxLength(20)]
        public String SoundexTitle { get; set; }

        public ItemStatus Status { get; set; } = 0;

        [SQLite.MaxLength(50000)]
        public String Description { get; set; }

        [Indexed, SQLite.MaxLength(2000)]
        public String FullFilename { get; set; }

        // Movie Chrono data
        public int Year { get; set; }
        public bool Serie { get; set; }
        public int Season { get; set; }
        public int Episode { get; set; }

        // Movie Attributes
        public TimeSpan? Duration { get; set; }
        public int? Resolution { get; set; }

        // control flags
        public int ToRemoveFromCatalog { get; set; } = 0; // Needs to remove folders from catalogs folder
                                                          // this flag indicates that has many unnecessary
                                                          // folders at tmdb catalog folders
        public int TmdbPosterFoldersQtde { get; set; }
        public int ToDelete { get; set; } = 0;
        public int ConfirmDelete { get; set; } = 0;
        public int Deleted { get; set; } = 0;
        public int Inserted { get; set; } = 1;
        public int NeedTmdb { get; set; } = 1;
        public int ManualIntervention { get; set; } = 0;

        // Fille Attribs
        [SQLite.MaxLength(100)]
        public String VolumeName { get; set; }

        [Indexed, SQLite.MaxLength(100)]
        public uint? VolumeId { get; set; }

        public long UnsudedSpace { get; set; } // only for Volumes

        public long Size { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? CreationDate { get; set; }

        // Captured data
        [SQLite.MaxLength(200)]
        public String TmdbMainMovieFolder { get; set; }

        [SQLite.MaxLength(50000) ]
        public String JsonFileInfo { get; set; } // al filesystem file info
        [SQLite.MaxLength(50000)]
        public String JsonCRC { get; set; }
        [SQLite.MaxLength(50000)]
        public String JsonMoviesData { get; set; }


        [SQLite.Ignore, TypeConverter(typeof(ExpandableObjectConverter))]
        public List<CatalogItem> Items { get; set; }

        [SQLite.Ignore]
        public FileInfo FileInfo { get; set; }

        [SQLite.Ignore]
        public DriveInfo DiveInfo { get; set; }

        [SQLite.Ignore]
        public CatalogItem parent { get; set; }

    }

    public class MovieImage
    {
        public String Key { get; set; }
        public String Filename { get; set; }
        public Bitmap Image { get; set; }
    }

    public enum ManualFieldsIntervention
    {
        Title,
        Year,
        Serie,
        Season,
        Episode
    }

    // this class is used to retrieve updated data
    // when in automatic processess
    public class ManualIntervention
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public ManualFieldsIntervention Field { get; set; }

        [Indexed, SQLite.MaxLength(2000)]
        public string OriginalValue { get; set; }

        [Indexed, SQLite.MaxLength(2000)]
        public string Value { get; set; }
    }

}