# How Git Works

## Object Types

* commit
  * ref to a tree
  * ref to 1 or more parents
* tree
  * a list containing trees and blobs
  * trees contain filenames
* blob
  * binary content of a filename
  * contains no filename information
* annotated tag
  * points to a commit
  * contains a message

## Layers of the Onion

From inside out

* persistent map
  * a collection of keys and values stored in a database
    * keys are sha1 hashes
    * values are any sequence of bytes
      * commit, file, directory, a-tag, etc
* stupid content tracker
  * stores commitment objects
    * contain trees and blobs
    * contains info about parent commitments
  * works as a versioned file system
* revision control system
* distributed revision control system

## Helper commands

### Hash-Object

Creates a hash from a given file

```sh
git hash-object test.txt
```

### Cat-File

Prints information about the given sha1 hash object

```sh
# prints the type of object
git cat-file -t c24f14a3a4297b057cb03cbf22f90b084fccd1ab
# prints the formatted data of the object
git cat-file -p c24f14a3a4297b057cb03cbf22f90b084fccd1ab
```
