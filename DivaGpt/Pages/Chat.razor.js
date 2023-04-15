export function gotoBottom() {
    let elm = document.documentElement;

    // scrollHeight ページの高さ clientHeight ブラウザの高さ
    let bottom = elm.scrollHeight - elm.clientHeight;

    // 垂直方向へ移動
    window.scroll(0, bottom);

    // 入力欄にフォーカスを当てる
    document.getElementById("chatInput").focus();
}

