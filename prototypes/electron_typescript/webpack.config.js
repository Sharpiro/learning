//@ts-nocheck

const path = require("path")
var HtmlWebpackPlugin = require('html-webpack-plugin')

module.exports = {
  mode: "development",
  target: 'electron-main',
  node: {
    __dirname: true
  },
  devtool: "inline-source-map",
  entry: {
    index: "./app/index.ts",
    menu: "./app/menu.ts",
    // renderer: "./app/renderer.ts"
  },
  output: {
    filename: "[name]-bundle.js",
    path: path.join(__dirname, "build")
  },
  resolve: {
    // Add `.ts` and `.tsx` as a resolvable extension.
    extensions: [".ts", ".tsx", ".js"]
  },
  module: {
    rules: [
      // all files with a `.ts` or `.tsx` extension will be handled by `ts-loader`
      {
        test: /\.tsx?$/,
        loader: "ts-loader"
      }
    ]
  },
  plugins: [
    new HtmlWebpackPlugin({
      hash: false,
      template: './app/index.html',
      filename: 'index.html'
    })
  ]
}
