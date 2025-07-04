﻿namespace HKCCinemas.Helper
{
    public class RandomAvatar
    {
        private readonly string[] avatars =
        {
            "https://png.pngtree.com/png-clipart/20231015/original/pngtree-man-avatar-clipart-illustration-png-image_13302502.png",
    "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSIyUWMc-J3Kv8BDo98oaEheOFsKW4VD293XI6jZTvITJIpdr37f_QInRp10jUBsqEM0nk&usqp=CAU",
    "https://png.pngtree.com/png-vector/20240204/ourlarge/pngtree-avatar-job-businessman-flat-portrait-of-man-png-image_11608099.png"
        };
        public string GenerateRandomAvatar()
        {
            Random random = new Random();
            return avatars[random.Next(avatars.Length - 1)];
        }
    }
}
