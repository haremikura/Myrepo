﻿$(function () {
    $(".js_focus").focus();
})

global = {
    value: {},
    setValue: function (name, value) {
        this.value[name] = value;
    },
    getValue: function (name) {
        return this.value[name];
    }
}