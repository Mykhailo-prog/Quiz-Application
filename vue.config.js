module.exports = {
     publicPath: process.env.BASE_URL,
     //TODO: Strange name for output build folder...
     outputDir: '../../builds/$safeprojectname$',
     devServer: {
          watchOptions: {
               poll: true
          }
     }
}