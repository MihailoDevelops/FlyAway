using FlyAway.Api.Core;
using FlyAway.Application;
using FlyAway.Application.Commands;
using FlyAway.Application.Queries;
using FlyAway.DataAccess;
using FlyAway.Implementation.Commands;
using FlyAway.Implementation.Logging;
using FlyAway.Implementation.Queries;
using FlyAway.Implementation.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<FlyAwayContext>();

//Commands
builder.Services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
builder.Services.AddTransient<IDeleteCategoryCommand, EfDeleteCategoryCommand>();
builder.Services.AddTransient<IUpdateCategoryCommand, EfUpdateCategoryCommand>();
builder.Services.AddTransient<ICreateTagCommand, EfCreateTagCommand>();
builder.Services.AddTransient<IDeleteTagCommand, EfDeleteTagCommand>();
builder.Services.AddTransient<IUpdateTagCommand, EfUpdateTagCommand>();
builder.Services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
builder.Services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
builder.Services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
builder.Services.AddTransient<ICreatePostCommand, EfCreatePostCommand>();
builder.Services.AddTransient<IDeletePostCommand, EfDeletePostCommand>();
builder.Services.AddTransient<IUpdatePostCommand, EfUpdatePostCommand>();
builder.Services.AddTransient<ICreateCommentCommand, EfCreateCommentCommand>();
builder.Services.AddTransient<IDeleteCommentCommand, EfDeleteCommentCommand>();
builder.Services.AddTransient<ICreateCommentLikeCommand, EfCreateCommentLikeCommand>();
builder.Services.AddTransient<IDeleteCommentLikeCommand, EfDeleteCommentLikeCommand>();
builder.Services.AddTransient<ICreateUserUseCaseCommand, EfCreateUserUseCaseCommand>();
builder.Services.AddTransient<IDeleteUserUseCaseCommand, EfDeleteUserUseCaseCommand>();

//Queries
builder.Services.AddTransient<IGetCategoriesQuery, EfGetCategoriesQuery>();
builder.Services.AddTransient<IGetTagsQuery, EfGetTagsQuery>();
builder.Services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
builder.Services.AddTransient<IGetPostsQuery, EfGetPostsQuery>();
builder.Services.AddTransient<IGetCommentsQuery, EfGetCommentsQuery>();
builder.Services.AddTransient<IGetCommentLikesQuery, EfGetCommentLikesQuery>();
builder.Services.AddTransient<IGetSingleCategoryQuery, EfGetSingleCategoryQuery>();
builder.Services.AddTransient<IGetSingleTagQuery, EfGetSingleTagQuery>();
builder.Services.AddTransient<IGetSinglePostQuery, EfGetSinglePostQuery>();
builder.Services.AddTransient<IGetSingleCommentQuery, EfGetSingleCommentQuery>();
builder.Services.AddTransient<IGetSingleUserQuery, EfGetSingleUserQuery>();
builder.Services.AddTransient<IGetLogEntriesQuery, EfGetLogEntriesQuery>();
builder.Services.AddTransient<IGetUserUseCasesQuery, EfGetUserUseCasesQuery>();
builder.Services.AddTransient<IGetSingleUserUseCaseQuery, EfGetSingleUserUseCaseQuery>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IApplicationActor>(x => 
{
    var accessor = x.GetService<IHttpContextAccessor>();

    var user = accessor.HttpContext.User;
    if (user.FindFirst("ActorData") == null)
        throw new InvalidOperationException("Actor data doesn't exist in token");

    var actorString = user.FindFirst("ActorData").Value;
    var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);
    return actor;
});
builder.Services.AddTransient<UseCaseExecutor>();
builder.Services.AddTransient<IUseCaseLogger, EfUseCaseLogger>();
builder.Services.AddTransient<JwtManager>();

//Validators
builder.Services.AddTransient<CreateCategoryValidator>();
builder.Services.AddTransient<UpdateCategoryValidator>();
builder.Services.AddTransient<CreateTagValidator>();
builder.Services.AddTransient<UpdateTagValidator>();
builder.Services.AddTransient<CreateUserValidator>();
builder.Services.AddTransient<UpdateUserValidator>();
builder.Services.AddTransient<CreatePostValidator>();
builder.Services.AddTransient<UpdatePostValidator>();
builder.Services.AddTransient<CreateCommentValidator>();
builder.Services.AddTransient<CreateCommentLikeValidator>();
builder.Services.AddTransient<CreateUserUseCaseValidator>();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = "flyaway_api",
            ValidateIssuer = true,
            ValidAudience = "Any",
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is a secret key for authentification")),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
