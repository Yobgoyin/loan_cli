
    const validations = [
        {
            key: 'mobile',
            label: 'Mobile',
            validator: 'required, mobile'
        },
        {
            key: 'password',
            label: 'Password',
            validator: 'required, password, minRule, maxRule',
            minRule: {
                validate(value) {
                        return value.trim().length >= 6;
                },
                message: '%s minLength >= 6'
            },
            maxRule: {
                validate(value) {
                    return value.trim().length <= 8;
                },
                message: '%s maxLength <= 8'
            }
        },
        {
            key: 'repassword',
            label: 'Repeat Password',
            validator: 'required, password, repasswordRule',
            repasswordRule: {
                validate(value, data) {
                    return value === data.password;
                },
                message: 'repassword !== password'
            }
        },
        {
            key: 'gender',
            label: 'Gender',
            validator: 'required'
        }
    ];
