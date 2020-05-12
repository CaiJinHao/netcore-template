[Required]
[StringLength(50,MinimumLength =10)]
[MaxLength(50)]
[DataType(DataType.DateTime)]
[Compare("su_pwd", ErrorMessage = "二次密码不一致")]

内置验证：
[CreditCard]：验证属性是否有信用卡格式。
[Compare]：验证模型中的两个属性是否匹配。
[EmailAddress]：验证属性是否有电子邮件格式。
[Phone]：验证属性是否有电话号码格式。
[Range]：验证属性值是否在指定范围内。
[RegularExpression]：验证属性值是否与指定的正则表达式匹配。
[Required]：验证字段是否非 NULL。 请参阅 [Required] 属性，获取关于该特性的行为的详细信息。
[StringLength]：验证字符串属性值是否未超过指定长度限制。
[Url]：验证属性是否有 URL 格式。
[Remote]：通过调用服务器上的操作方法，验证客户端上的输入。 请参阅 [[Remote]` 属性(#remote-attribute)，获取关于该特性的行为的详细信息。
内置验证完整列表
https://docs.microsoft.com/zh-cn/dotnet/api/system.componentmodel.dataannotations?view=netframework-4.8


类继承该接口IValidatableObject 类级验证
public class sys_user_request_model : sys_user_model, IValidatableObject
    {
        /// <summary>
        /// 获取数据选项 枚举类型
        /// </summary>
        public int Options { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>		
        [Compare("su_pwd", ErrorMessage = "二次密码不一致")]
        public string ConfirmPwd { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ConfirmPwd == su_pwd && su_username == "jinhaouser")
            {
                yield return new ValidationResult($"密码相同用户名相同比对成功.");
            }
        }
    }

映射：
Table 表名    只有一个
Key 主键      只有一个(可能会有多个字段有Key，但这里不考虑)
