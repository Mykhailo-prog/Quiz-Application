module.exports = {
     publicPath: process.env.BASE_URL,
     outputDir: '../../builds/$safeprojectname$',
     devServer: {
          watchOptions: {
               poll: true
          }
     }
}