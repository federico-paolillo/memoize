const Rendering = require('./rendering');

const typeOfOutput = process.argv[2];
const genericArgumentsCount = Number(process.argv[3]);

typeOfOutput === 'c' ? 
    Rendering.renderMemoizerClassAsync(genericArgumentsCount).then(console.log) :
    Rendering.renderMemoizerTestAsync(genericArgumentsCount).then(console.log) ;