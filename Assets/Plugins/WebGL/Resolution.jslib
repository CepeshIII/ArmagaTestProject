mergeInto(LibraryManager.library, {
    SetUnityResolution: function(width, height, isFullScreen) {
        var canvas = document.querySelector("#unity-canvas");
        if (!canvas) return;

        canvas.width = width;
        canvas.height = height;

        if (isFullScreen) {
            canvas.style.width = "100vw";
            canvas.style.height = "100vh";
            canvas.style.margin = "0"; 
        } else {
            canvas.style.width = width + "px";
            canvas.style.height = height + "px";
            
            // Centering logic
            canvas.style.display = "block";
            canvas.style.margin = "0 auto"; // Centers horizontally
            
            // Optional: If you want it vertically centered in the viewport
            canvas.style.position = "absolute";
            canvas.style.top = "50%";
            canvas.style.left = "50%";
            canvas.style.transform = "translate(-50%, -50%)";
        }
    }
});