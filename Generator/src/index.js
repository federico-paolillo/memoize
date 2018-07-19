const Rendering = require('./rendering');

const genericArgumentsCount = Number(process.argv[2]);

Rendering.renderMemoizerClassAsync(genericArgumentsCount).then(console.log);