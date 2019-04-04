# Mastering Git

## The Four Areas: Introduction

### The Four Areas

* working area
* index (staging)
* repository
* stash

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
  * repository
    * doesn't change data
    * moves `HEAD` to a different `branch` or `commit`
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
