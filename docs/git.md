# Git

## How Git Works

### Object Types

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

### Layers of the Onion

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

## dot syntax

<!-- commit ranges -->
* log
  * double dot
    * `git log A..B`
    * all commits in `B` that aren’t in `A`
    * all commits in `B` since common ancestor of `A` and `B`
    * all commits as if `B` is merged into `A` (3-way merge)
  * triple dot
    * `git log --left-right A...B`
    * all commits in *either* but *not both* (XOR)
    * all commits in *either* since common ancestor of `A` and `B`
* diff
  * double dot
    * `git diff A..B`
    * compare tree of `A` to tree of `B`
  * triple dot
    * `git diff A...B`
    * compare tree of common ancestor of `A` and `B` to tree of `B`
    * compare tree changes by `B` when `B` is merged into `A` (3-way merge)
* refs
  * [git diff dot syntax](https://stackoverflow.com/a/7252067/5344498)
  * [Using Commit Ranges with Git Log](https://stackoverflow.com/a/24186641/5344498)
  * [commit ranges](https://git-scm.com/book/en/v2/Git-Tools-Revision-Selection#_commit_ranges)

<!-- markdownlint-disable MD033 -->
<img src="content/git/git_dot_syntax.png" width="800px" alt="https://stackoverflow.com/a/46345364/5344498">
<!-- markdownlint-enable MD033 -->

## diff

### diff file with working

```sh
git diff ef5d00179b812df10a3f2e7b7ca5ac8a6e26f732 -- test_client/config
```

### diff files changed between commits

```sh
git diff --name-status master~2..HEAD
```

## log

### log last commits b/w x and y

```sh
git log --oneline master~5..master~3
```

## log pretty

```sh
git log --pretty=format:"%h - %ae, %ce : %s" -1
```

## log count

```sh
git rev-list --count master
```

## interactive amend commit

```sh
git rebase -i aabbccff
# select 'e' to edit every affected commit

# amend the author for each commit
git commit --amend --author="sharpiro <dsharpbb09@gmail.com>"

# continue through each selected commit
git rebase --continue
```

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

## advanced commands

### move folder to new repository

```sh
git filter-branch --subdirectory-filter <directory 1> -- --all
```

### remove file from history

```sh
git filter-branch --force --index-filter \
'git rm --cached --ignore-unmatch sniffing_proxy/test_client/config' \
--prune-empty --tag-name-filter cat -- --all

```

### remove folder from history

```sh
git filter-branch --tree-filter 'rm -rf sniffing_proxy' --prune-empty HEAD
```

## The Four Areas: Introduction

### The Four Areas

* working area
  * your own private area where you interact with your data
* index (staging area)
  * decouples the working area and repository, letting you decide when and how to move data to the repository
* repository
  * safe and versioned area for your data
* stash
  * like a clipboard for your changes temporarily

### Git Commands

Most commands in git can be understood by asking 2 questions:

1. How does this command move information across the Four Areas?
1. How does this command change the Repository?

### Terms

* `HEAD` points to a `branch`
* `branch` points to a `commit`
* `commit` points to a `tree` or `file`

### Git Diff

compares working area with index

```bash
git diff
```

compares index with repository

```bash
git diff --cached
```

## The Four Areas: Basic Workflow

* `add`
  * copies from working area to index
* `commit`
  * copies from working area to repository
* `checkout`
  * moves `HEAD` in-place or to a different `branch` or `commit`
  * then copies current data from repository to the working area and index
* `rm` (remove)
  * removes from *both* `working area` and `index`
  * `--cached` removes from `index` only
    * AKA "un-stage"
* `mv`
  * manual moving of files

## The Four Areas: Reset

* `reset`
  * moves a branch to a different commit
  * doesn't move `HEAD`
    * `HEAD` is pointing at the same branch but at a different commit
  * `--hard`
    * copies data from the repository to *both* the `index` and `working area`
  * `--mixed`
    * copies data from the repository to *only* the `index`
    * default option
  * `--soft`
    * only moves the branch, doesn't copy files

```sh
# un-stages 1 or more files by moving HEAD in-place (not moving)

# copies the file or files to the index
git reset HEAD <file>(optional)

# copies the file or files to the index and the working area
git reset -hard HEAD <file>(optional)
```

## History: Fixing Mistakes

### Git Log

```sh
git log --graph --oneline --decorate
```

```sh
# log all of the commits we would gain if we merged master into no-good
git log no-good..master --oneline
```

### Git Show

detailed information about a commit, branch, or head

```sh
# show information about the parent of head
git show head^
```

```sh
# show information about the parent of parent of head
# git show head^^
git show head~2
```

```sh
# show information about the 2nd parent of a commit 2 from head
# git show head^^
git show head~2^2
```

### Git Blame

See the last author to change a line

```sh
# carrot means the commit that added the file to the repo
^0bce6d3 (sharpiro 2019-04-05 13:26:20 -0400 1) hello_world
bc79665b (sharpiro 2019-04-05 13:53:45 -0400 2) master_add
bc79665b (sharpiro 2019-04-05 13:53:45 -0400 3) test_add
```

## Fixing Mistakes

### Git Reflog

`git reflog` is a log of the movements of `head`

## Workflows

Don't just adopt a workflow from the internet.  Start with a simple workflow, and then add complexity only as-needed

### Distribution Models

* peer to peer model
* centralized model
* pull request model
* dictator and lieutenants model

### Branching Models

* unstable branch
  * head may or may not work
* stable branch
  * head is stable

#### How to share a commit

* cherry-pick
  * only copies changes, doesn't share commits
* create a third "merge branch"
  * ex:
    * 2 branches, `release`, and `integration` that have branched off `master` in the past
    * we have a new code change that we need to get into both branches
    * create a 3rd "merge branch" called `hotfix` off of `master`
    * commit the new changes to `hotfix`
    * merge `hotfix` into `release`
    * merge `hotfix` into `integration`
