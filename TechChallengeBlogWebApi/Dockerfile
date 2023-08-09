FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /usr/src/app
COPY package*.json ./
RUN npm install
COPY . .
EXPOSE 8080
CMD ["dotnet", "TechChallengeBlogWebApi.dll"]
