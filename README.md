# search-pdfs-with-text

A desktop application built with **Angular** + **Tauri** that searches for a keyword inside the content of PDF files. Just type the term you're looking for into an input, and the app scans your PDFs for that text — including scanned PDFs, thanks to built-in OCR support.

## ✨ Features

- Search for text/keywords inside PDF files
- OCR support (via Tesseract.js) for scanned PDFs with no text layer
- PDF reading and parsing (pdf.js, pdf-parse, pdf-lib)
- Lightweight, native desktop UI packaged with Tauri

## 🛠️ Tech stack

- [Angular](https://angular.io/) 17
- [Angular Material](https://material.angular.io/)
- [Tauri](https://tauri.app/) 1.x (Rust)
- [pdf.js](https://mozilla.github.io/pdf.js/) / `pdfjs-dist`
- [pdf-lib](https://pdf-lib.js.org/)
- [pdf-parse](https://www.npmjs.com/package/pdf-parse)
- [tesseract.js](https://tesseract.projectnaptha.com/) (OCR)
- [ng2-pdf-viewer](https://www.npmjs.com/package/ng2-pdf-viewer)

## ✅ Prerequisites

- [Node.js](https://nodejs.org/) (version specified in `.node-version`)
- [Rust](https://www.rust-lang.org/tools/install) and the Tauri toolchain
- Tauri's system dependencies for your OS ([official guide](https://tauri.app/v1/guides/getting-started/prerequisites))

## 🚀 Installation

```bash
# Clone the repository
git clone https://github.com/leogamaz/search-pdfs-with-text.git
cd search-pdfs-with-text

# Install dependencies
npm install
```

## ▶️ Usage

Run in development mode (opens the Tauri window with Angular hot-reload):

```bash
npm start
```

Run only the Angular front end in the browser:

```bash
npm run web:serve
```

Build the front end for production:

```bash
npm run web:prod
```

Build the desktop app executable/installer:

```bash
npm run tauri:bundle
npm run tauri:bundle
```

## 📁 Project structure

```
├── src/            # Angular application source code
├── src-tauri/       # Tauri Rust configuration and code
├── .github/         # GitHub workflows/configuration
├── angular.json     # Angular CLI configuration
├── package.json     # Scripts and dependencies
└── tsconfig.json    # TypeScript configuration
```

## 🧪 Tests

```bash
npm test
```

## 🤝 Contributing

Contributions are welcome! Feel free to open an issue or submit a pull request. Please check the [Code of Conduct](CODE_OF_CONDUCT.md) before contributing.

## 📄 License

This project is licensed under the terms described in [LICENSE.md](LICENSE.md).
