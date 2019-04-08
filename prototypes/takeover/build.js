const Boom = require('./boom');
const path = require('path');
const fs = require("fs")
var uglify = require("uglify-es");

const vData = fs.readFileSync(`${__dirname}/boom.js`).toString()
const minifyOptions = { mangle: { toplevel: true, reserved: ["execute"] }, compress: {} }
const vDataMin = uglify.minify(vData, minifyOptions).code
const outFileName = path.resolve(__dirname, "dist", "boom-dist.js")
const minFileName = path.resolve(__dirname, "dist", "temp-min.txt")

fs.writeFileSync(minFileName, vDataMin)

const vDataBase64 = Buffer.from(vDataMin).toString("base64")
const prefixBuffer = Boom.cpb(outFileName)
const payloadBuffer = Boom.cp(vDataBase64)
const fullBuffer = Buffer.concat([prefixBuffer, payloadBuffer])

fs.writeFileSync(outFileName, fullBuffer)
