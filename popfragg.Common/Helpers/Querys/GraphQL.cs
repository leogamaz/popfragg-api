using Serilog;
using System;

namespace popfragg.Common.Helpers.Querys
{
    public static class GraphQL
    {
        public static string GetSignUpQuery()
        {
            return @"
                mutation signup($data: SignUpInput!) {
                  signup(params: $data) {
                    message
                    access_token
                    expires_in
                    refresh_token
                    id_token
                    should_show_email_otp_screen
                    should_show_mobile_otp_screen
                    should_show_totp_screen
                    authenticator_scanner_image
                    authenticator_secret
                    authenticator_recovery_codes
                    user {
                      id
                      email
                      email_verified
                      given_name
                      family_name
                      middle_name
                      nickname
                      preferred_username
                      picture
                      signup_methods
                      gender
                      birthdate
                      phone_number
                      phone_number_verified
                      roles
                      created_at
                      updated_at
                      is_multi_factor_auth_enabled
                      app_data
                    }
                  }
                }
            ";
        }

        public static string GetSignInQuery()
        {
            return """
            mutation login($email: String!, $password: String!) {
                login(params: { email: $email, password: $password }) {
                user {
                    id
                    email
                    email_verified
                    given_name
                    family_name
                    middle_name
                    nickname
                    preferred_username
                    picture
                    signup_methods
                    gender
                    birthdate
                    phone_number
                    phone_number_verified
                    roles
                    created_at
                    updated_at
                    is_multi_factor_auth_enabled
                    app_data
                }
                access_token
                expires_in
                message
                }
            }
            """;
        }

    }
}
