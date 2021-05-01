$(document).ready(function () {
    $('input[type="file"]').on("change", function () {
        const fileNames = [];
        const files = document.getElementById("fileInput").files;
        if (files.length > 1) {
            fileNames.push("Total Files (" + files.length + ")");
        } else {
            for (const file in files) {
                if (files.hasOwnProperty(file)) {
                    fileNames.push(files[file].name);
                }
            }
        }
        $(this)
            .next(".custom-file-label")
            .html(fileNames.join(","));
    });
});