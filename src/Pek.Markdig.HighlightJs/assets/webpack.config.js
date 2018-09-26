const path = require('path');

module.exports = {
  mode: 'production',
  entry: './server.js',
  output: {
    path: path.resolve(__dirname, '..', 'Resources'),
    libraryTarget: 'this'
  }
};
