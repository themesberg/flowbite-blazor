/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "App.razor",
        "./wwwroot/**/*.{razor,html,cshtml,cs}",
        "./Layout/**/*.{razor,html,cshtml,cs}",
        "./Pages/**/*.{razor,html,cshtml,cs}",
        "./Components/**/*.{razor,html,cshtml,cs}",
    ],
    darkMode: 'class',
    theme: {
        extend: {
            colors: {
                primary: { "50": "#eff6ff", "100": "#dbeafe", "200": "#bfdbfe", "300": "#93c5fd", "400": "#60a5fa", "500": "#3b82f6", "600": "#2563eb", "700": "#1d4ed8", "800": "#1e40af", "900": "#1e3a8a", "950": "#172554" }
            },
            maxWidth: {
                "8xl": "90rem",
            },
            keyframes: {
                fadeIn: {
                    "0%": { opacity: "0" },
                    "100%": { opacity: "1" },
                },
            },
            animation: {
                "fade-in": "fadeIn 200ms ease-in-out",
            },
            boxShadow: {
                "lg-light": "0 10px 15px -3px rgba(255, 255, 255, 0.1), 0 4px 6px -2px rgba(255, 255, 255, 0.05)",
            },
        },
        fontFamily: {
            'body': [
                'Inter',
                'ui-sans-serif',
                'system-ui',
                '-apple-system',
                'system-ui',
                'Segoe UI',
                'Roboto',
                'Helvetica Neue',
                'Arial',
                'Noto Sans',
                'sans-serif',
                'Apple Color Emoji',
                'Segoe UI Emoji',
                'Segoe UI Symbol',
                'Noto Color Emoji'
            ],
            'sans': [
                'Inter',
                'ui-sans-serif',
                'system-ui',
                '-apple-system',
                'system-ui',
                'Segoe UI',
                'Roboto',
                'Helvetica Neue',
                'Arial',
                'Noto Sans',
                'sans-serif',
                'Apple Color Emoji',
                'Segoe UI Emoji',
                'Segoe UI Symbol',
                'Noto Color Emoji'
            ],
            'mono': [
                'ui-monospace',
                'SFMono-Regular',
                'MesloLGL Nerd Font Mono',
                'Cascadia Mono',
                'Courier New'
            ]
        }
    },
    plugins: [
        require('flowbite/plugin')
    ],
}