Serve.Run(RunOptions.Default
    .ConfigureBuilder(builder => builder.WebHost
        .UseUrls("http://*:8079")
        .UseKestrel(option =>
        {
            // ���� api ��ʱ����ʱ��
            option.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(10);
            option.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(10);
        })
    )
);