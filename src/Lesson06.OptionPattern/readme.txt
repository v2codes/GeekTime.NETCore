ѡ��ģʽ(���) -- ��������������õ����ʵ��

    �����ռ䣺Microsoft.Extensions.Options
    ע�����ʱ��ָ�����ã�services.Configure<OrderServiceOptions>(Configuration.GetSection("OrderService"));��

����
    ֧�ֵ���ģʽ��ȡ����
    ֧�ֿ���
    ֧�����ñ��֪ͨ
    ֧������ʱ��̬�޸�ѡ��ֵ
    
���ԭ��
    �ӿڷ���ԭ��ISP�����಻Ӧ����������ʹ�õ�����
    ��ע����루SoC������ͬ�����������֮������ò�Ӧ�໥���������

����
    Ϊ���ǵķ������ XXXOptions 
    ʹ��IOptions<XXXOptions>��IOptionsSnapshot<XXXOptions>��IOptionsMonitor<XXXOptions> ��Ϊ����Ĺ��캯���Ĳ���
        

ѡ�������ȸ��� -- �÷����֪���õı仯
    �ؼ���
        IOptionsMonitor<out TOptions>
        IOptionsSnapshot<out TOptions>

    ʹ�ó���
        ��Χ����������ʹ�� IOptionsSnapshot
        ��������ʹ�� IOptionsMonitor

    ͨ���������ѡ��
        IPostConfigureOptions<TOptions>
        // ��̬��������������ļ���������Ч
        // ʵ��������Ʒ����ʱ�򣬻�����һЩ�������󣬱�������ö�ȡ����֮����Ҫ���ڴ������һЩ���⴦����ô�Ϳ���ʹ�ö�̬���õķ�ʽ
        services.PostConfigure<OrderServiceOptions>(option =>
        {
            option.MaxOrderCount = 1000;
        });   
            

Ϊѡ�����������֤ -- ����������õ�Ӧ�ý����û�����
    ������֤����
        ֱ��ע����֤����
        ʵ�� IValidateOptions<TOptions>
        ʹ�� Microsoft.Extensions.Options.DataAnnotations
    
    ͨ�����ѡ����֤�����������ô�����������֯Ӧ�ó����������������Ϳ��Ա����û������ﵽ����Ľڵ㣨�����ϡ�