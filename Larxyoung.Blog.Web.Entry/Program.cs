Serve.Run(RunOptions.Default
    .ConfigureBuilder(builder => builder.WebHost
        .UseUrls("http://*:8079")
        .UseKestrel(option =>
        {
            // 设置 api 超时请求时间
            option.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(10);
            option.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(10);
        })
    )
);