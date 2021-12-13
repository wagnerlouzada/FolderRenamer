using System;

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
        public long Id { get; set; }
        public ItemType Type { get; set; }
        public string Name { get; set; }
        public string NameSearched { get; set; }

        public long FatherId { get; set; }
        public ItemStatus Status { get; set; } = 0;

        public string Description { get; set; }

        // Captured data
        public FileInfo FileInfo { get; set; }
        public String JsonFileInfo { get; set; } // al filesystem file info

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DriveInfo DiveInfo { get; set; }

        public String JsonCRC { get; set; }

        public List<CatalogItem> Items { get; set; }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public List<SearchParm> SearchParm { get; set; }

        //[TypeConverter(typeof(ExpandableObjectConverter))]
        //public Movies MoviesData { get; set; }
        public string JsonMoviesData { get; set; }

        // Chrono data
        public int Year { get; set; }
        public bool Serie { get; set; }
        public int Season { get; set; }
        public int Episode { get; set; }

        // control flags
        public int ToRemoveFromCatalog { get; set; } = 0;
        public int ToDelete { get; set; } = 0;
        public int ConfirmDelete { get; set; } = 0;
        public int Deleted { get; set; } = 0;
        public int Inserted { get; set; } = 1;

        //public CatalogItem()
        //{
        //    SubItens = new List<CatalogItem>();
        //}
    }

    public class VideoFolder
    {
        String FullPath { get; set; }
        String Path { get; set; }
        String NewPath { get; set; }
        Boolean Modified { get; set; } = false;
    }

    public class VideoFile
    {
        String FullFilename { get; set; }
        String Filename { get; set; }
        String Extension { get; set; }
        String Path { get; set; }
        String NewFileName { get; set; }
        Boolean Modified { get; set; } = false;
    }

    public class SubtitleFile
    {
        String FullFilename { get; set; }
        String Filename { get; set; }
        String Extension { get; set; }
        String Path { get; set; }
        String NewFileName { get; set; }
        Boolean Modified { get; set; } = false;
    }
}