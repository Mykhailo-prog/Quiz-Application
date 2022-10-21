module.exports = {
  publicPath: process.env.BASE_URL,
  //TODO: Strange name for output build folder...
  //DONE
  outputDir: "../../builds/QuizBuild",
  devServer: {
    watchOptions: {
      poll: true,
    },
  },
};
