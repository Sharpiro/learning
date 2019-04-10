# Mastering Git

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
