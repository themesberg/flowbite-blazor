import resolve from '@rollup/plugin-node-resolve';
import terser from '@rollup/plugin-terser';

export default {
    input: 'js-src/index.js',
    output: {
        file: 'wwwroot/js/floating-ui.bundle.js',
        format: 'iife',
        name: 'FlowbiteFloatingBundle',
        sourcemap: false
    },
    plugins: [
        resolve({
            browser: true
        }),
        terser({
            format: {
                comments: false
            }
        })
    ]
};
