const path = require("path");

module.exports = {
  entry: path.resolve(__dirname, "src", "index.js"),
  output: {
    path: path.resolve(__dirname, "dist"),
    filename: "bundle.js",
    publicPath: "/"
  },
  devServer: {
    static: {
      directory: path.resolve(__dirname, "public"),
    },
    historyApiFallback: true,
    port: 3000
  },
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/,
        exclude: /node_modules/,
        use: ["babel-loader"]
      },
<<<<<<< HEAD
        {
          test: /\.css$/i,
          use: ["style-loader", "css-loader"],
        },
      ],
    },
=======
      {
        test: /\.css$/i,
        use: ["style-loader", "css-loader"],
      },
    ],
  },
>>>>>>> adf610e8f9ca722e3652c06ec8be069a86157d8a
  resolve: {
    extensions: [".js", ".jsx"]
  }
};
