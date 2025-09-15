window.printCustom = function (divId) {
    // Get settings from the side panel
    const copies = parseInt(document.getElementById('copies-input')?.value || 1);
    const layout = document.querySelector('input[name="layout"]:checked')?.value || 'portrait';
    const pages = document.querySelector('input[name="pages"]:checked')?.value || 'all';

    // Get the div to print
    const divToPrint = document.getElementById(divId);
    if (!divToPrint) return;

    // Open a new window and write content
    const newWin = window.open('', '_blank');
    newWin.document.write('<html><head><title>Purchase Order Print</title>');
    newWin.document.write('<link rel="stylesheet" href="/css/app.css" />');
    newWin.document.write('<style>');
    newWin.document.write('body { font-family: Arial, sans-serif; margin: 0; padding: 0; }');
    newWin.document.write('.print-container { width: 100%; margin: 0; padding: 0; font-size: 12pt; }');
    newWin.document.write('.print-container table { border-collapse: collapse; width: 100%; }');
    newWin.document.write('.print-container th, .print-container td { border: 1px solid #ccc; padding: 8px; text-align: left; }');
    newWin.document.write(`@page { size: ${layout === 'landscape' ? 'landscape' : 'portrait'}; margin: 1cm; }`);
    newWin.document.write('</style>');
    newWin.document.write('</head><body>');
    newWin.document.write(divToPrint.outerHTML);
    newWin.document.write('</body></html>');

    newWin.document.close();

    // Trigger print dialog with copies
    for (let i = 0; i < copies; i++) {
        if (i > 0) setTimeout(() => { }, 1000); // Delay between copies (non-blocking)
        newWin.print();
    }

    // Close the window after printing (optional, depends on browser behavior)
    setTimeout(() => newWin.close(), 1000);
};