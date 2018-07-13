const path = require('path')

const namespace = 'diagrammatically.electron_edge.api'

const baseNetAppPath = path.join(__dirname, '\\..\\diagrammatically.electron_edge.api\\bin\\Debug\\netcoreapp2.0')
process.env.EDGE_USE_CORECLR = 1;
process.env.EDGE_APP_ROOT = baseNetAppPath;

const edge = require('electron-edge-js');

const baseDll = path.join(baseNetAppPath, namespace + '.dll')

var typeName = namespace + '.OptionApi'
//'QuickStart.Core.LocalMethods'

const getCurrentTime = edge.func({
    assemblyFile: baseDll,
    typeName: typeName,
    methodName: 'GetCurrentOptions'
})

window.onload = function () {
    setTimeout(function () {
        getCurrentTime('', function (error, result) {
            if (error) throw error;
            const words = result
            .map(w => w.Word)
            .reduce((accumulator, currentValue) => accumulator + '|' + currentValue)
            document.getElementById("typing-word").setAttribute('text', result[0].Search)
            document.getElementById("suggestion-list").setAttribute('words', words )
        })
    }, 1500)
}