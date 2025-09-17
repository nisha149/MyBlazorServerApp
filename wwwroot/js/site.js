window.printCustom = async (elementId) => {
    const element = document.getElementById(elementId);
    if (element) {
        const printWindow = window.open('', '', 'height=600,width=800');
        if (printWindow) {
            printWindow.document.write('<html><head><title>Print Purchase Order</title>');
            printWindow.document.write('<link rel="stylesheet" href="/css/print.css" media="print" />');
            printWindow.document.write('</head><body>');
            printWindow.document.write(element.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            printWindow.close();
        } else {
            alert('Popup blocked. Please allow popups for this site.');
        }
    } else {
        console.error('Element not found:', elementId);
    }
};