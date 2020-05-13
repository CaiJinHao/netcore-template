(function init() {
    var remember_me_key='remember-me-key';
    var page={
        prompt:function(title,msg,time){
            $('.prompt .prompt-title').html(title);
            $('.prompt .prompt-msg').html(msg);
            $('.prompt').fadeIn();
            setTimeout(function(){
                $('.prompt').fadeOut();
            },time);
        },
        remember:function(){
            var login_info_str=localStorage.getItem(remember_me_key);
            if(login_info_str!=null)
            {
                var login_info=JSON.parse(login_info_str);
                $('#UserName').val(login_info.IotKey);
                $('#Password').val(login_info.IotSecret);
                $('#remember-me').prop('checked',true);
            }
        }
    };

    page.remember();

    $('#login-form').submit(function (event) {
        event.preventDefault();
        var remember_me=$('#remember-me').prop('checked');
        var login_info = {
            IotKey: $('#UserName').val(),
            IotSecret: $('#Password').val()
        };
        if(remember_me)
        {
            localStorage.setItem(remember_me_key,JSON.stringify(login_info));
        }
        $.ajax({
            url: '/api/restadmin',
            type: 'POST',
            data: JSON.stringify(login_info),
            contentType: 'application/json;charset=utf-8',
            success: function (api_result) {
                if (api_result.Code === 0) {
                    sessionStorage.setItem('restapi_session_key', JSON.stringify(api_result.Result));
                    location.href='/restapi/';
                } else {
                    page.prompt("登录错误提示",api_result.Msg,3000);
                }
            }
        });
    });
}());