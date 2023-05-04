async function inactiveTimer(dotnetHelper) {
    var timer;
    var elapsedTimeInMilisecondsBeforeLogout = 60 * 60 * 1000;

    document.onmousemove = resetTimer;
    document.onkeydown = resetTimer;

    async function resetTimer() {
        clearTimeout(timer)
        timer = await setTimeout(logout, elapsedTimeInMilisecondsBeforeLogout);
    }

    async function logout() {
        await dotnetHelper.invokeMethodAsync("Logout");
    }
}