const path = require('path')

const namespace = 'diagrammatically.electron_edge.api'

const baseNetAppPath = path.join(__dirname, '\\..\\diagrammatically.electron_edge.api\\bin\\Debug\\netcoreapp2.0')
process.env.EDGE_USE_CORECLR = 1;
process.env.EDGE_APP_ROOT = baseNetAppPath;

const edge = require('electron-edge-js');

const baseDll = path.join(baseNetAppPath, namespace + '.dll')

const typeName = namespace + '.OptionApi'

const getCurrentMatches = edge.func({
    assemblyFile: baseDll,
    typeName: typeName,
    methodName: 'GetCurrentMatches'
})
const startLogger = edge.func({
    assemblyFile: baseDll,
    typeName: typeName,
    methodName: 'StartLogger'
})

const loadMatches = function () {
    setTimeout(function () {
        getCurrentMatches('', function (error, result) {
            
            if (error) throw error;
            if (result && result.length) {
                const words = result
                    .map(w => w.Word)
                    .reduce((accumulator, currentValue) => accumulator + '|' + currentValue)
                    document.getElementById("typing-word").setAttribute('text', result[0].Search)
                    document.getElementById("suggestion-list").setAttribute('words', words)
            }
            else {
                document.getElementById("typing-word").setAttribute('text', 'input')
                document.getElementById("suggestion-list").setAttribute('words', '')
            }
            loadMatches()
        })
    }, 100)
}
document.getElementById("typing-word").setAttribute('text', 'typing')
document.getElementById("suggestion-list").setAttribute('words', 'typ|kim|test|typ|kim|test')
window.onload = function () {
    startLogger('', function (error, result) {
        if (error) throw error;
        setTimeout(function () { loadMatches() }, 300)
    })
}