export function showToastMessage(name) {
    $(document).ready(function () {
        $('#' + name).toast({ delay: 7500 });
        $('#' + name).toast({ animation: true });
        $('#' + name).toast('show');
        $('.btn-close').click(function () {
            $('#' + name).toast("hide");
        });
    });
}