export function gotoBottom() {
    let elm = document.documentElement;

    // scrollHeight �y�[�W�̍��� clientHeight �u���E�U�̍���
    let bottom = elm.scrollHeight - elm.clientHeight;

    // ���������ֈړ�
    window.scroll(0, bottom);

    // ���͗��Ƀt�H�[�J�X�𓖂Ă�
    document.getElementById("chatInput").focus();
}

