/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './{Components,Icons}/**/*.{html,js,razor}',
  ],
  theme: {
    extend: {},
  },
  plugins: [
    require('flowbite/plugin')
  ],
  darkMode: 'class',
}
