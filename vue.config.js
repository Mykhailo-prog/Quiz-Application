module.exports = {
  publicPath: process.env.BASE_URL,
  //TODO: Strange name for output build folder...
  outputDir: "../../builds/QuizAppBuild",
  devServer: {
    watchOptions: {
      poll: true,
    },
  },
};
