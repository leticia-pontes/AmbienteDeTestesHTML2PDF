﻿function downloadFile(fileName, byteArray) {
    const blob = new Blob([new Uint8Array(byteArray)], { type: 'application/pdf' });
    const url = URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = fileName;
    document.body.appendChild(a);
    a.click();
    setTimeout(() => {
        document.body.removeChild(a);
        URL.revokeObjectURL(url); // Liberar a URL
    }, 0);
}