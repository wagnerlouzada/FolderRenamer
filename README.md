#  Media-Catalog - Folder Renamer

The project started as a file and folder renamer. 

It is evolving into an application to catalog movies, adjusting the titles to make it possible to search for references in services such as TMDB (The Movie Database). 

Many improvements need to be implemented, such as the execution of the information capture process using threads. 

It is also interesting to implement a more effective search for series, in order to optimize the process. 

Another necessary implementation is the integration of subtitles to movies, to reduce the work of renaming them.

To clean up the names of files and folders, 3 lists of terms are used, which can be applied to files and folders, only folders and only files.

For the storage of data referring to movies and series, SQLite is being used.

However, to enable maintenance of backups and other actions, the images referring to posters of the titles are kept in folders.

In the future, the identification of duplicates will be implemented and for that a file with CRC completion is in the same folder as the file referring to the movie. This information will be used to compare the contents of the file, and thus make it possible to find duplicates even when the filenames are different.
