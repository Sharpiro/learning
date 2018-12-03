import * as fs from "fs"
var glob = require("glob")
// import { glob } from "glob"
// import { Glob } from "glob"
import { join } from "path";

const playlistdirectory = "./music"
const playlistOnedirectory = "./music/playlist1"

// const playlists = loadPlaylistsSimple(playlistdirectory)
// const songs = loadSongs(playlistOnedirectory)
const globx = loadSongsRecursive(playlistdirectory, "**/*.{mp3,mp4}")

// console.log(playlists)
// console.log(songs)
console.log(globx)

function loadPlaylistsSimple(directory: string) {
    const data = fs.readdirSync(directory, { withFileTypes: true })
        .filter(f => f.isDirectory())
    return data
}

function loadPlaylistsComplex(directory: string) {
    const data = fs.readdirSync(directory)
        .map(p => {
            const stats = fs.lstatSync(join(directory, p))
            return { name: p, stats: stats, isDirectory: stats.isDirectory() }
        })
        .filter(f => f.isDirectory)
    return data
}

function loadSongs(directory: string) {
    const data = fs.readdirSync(directory, { withFileTypes: true })
        .filter(f => f.isFile())
    return data
}

function loadSongsRecursive(directory: string, pattern: string) {
    const globPattern = `${directory}/${pattern}`
    console.log(globPattern);

    glob(globPattern, undefined, (er: any, files: any) => {
        console.log(files)
    })

    const data = fs.readdirSync(directory, { withFileTypes: true })
        .filter(f => f.isFile())
    return data
}