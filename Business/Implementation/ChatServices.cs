using Business.Services;
using DataBase.Core;
using DataBase.Core.Consts;
using DataBase.Core.Models;
using DataBase.Core.Models.Accounts;
using DataBase.Core.Models.Notifications;
//using DataBase.EF;
using DomainModels;
using DomainModels.DTO;
//using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementation
{
    public class ChatServices : IChatServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAcountService _acountService;
        private readonly UserConnectionManager _userConnectionManager;
        //private readonly ApplicationDbContext _DbContext;
        public ChatServices(IUnitOfWork unitOfWork, IAcountService acountService ,UserConnectionManager userConnectionManager /*, ApplicationDbContext DbContext*/)
        {
            _unitOfWork = unitOfWork;
            _acountService = acountService;
            _userConnectionManager = userConnectionManager;
            //_DbContext = DbContext;

        }
        public async Task<List<ChatDTO>> GetUserChat(Guid firstUser, Guid secondUser)
        {
            var chatForFirstUser =await  _unitOfWork.Chat.FindAllAsync(c=> c.SenderId == firstUser && c.ReciveId==secondUser);
            var chatForSecondUser =await _unitOfWork.Chat.FindAllAsync(c => c.SenderId == secondUser && c.ReciveId == firstUser);
            var AllChats = chatForFirstUser.ToList();
            AllChats.AddRange(chatForSecondUser.ToList());
            AllChats.OrderBy(i=>i.TimeStamp).ToList();
            var ChatDto = OMapper.Mapper.Map<List<DomainModels.DTO.ChatDTO>>(AllChats);
            return ChatDto;
        }

        public async Task<bool> MarkUserChatAsRead(Guid firstUser, Guid secoundUserId)
        {
            var chatForFirstUser = await _unitOfWork.Chat.FindAllAsync(c => c.SenderId == firstUser && c.ReciveId == secoundUserId);
            var chatForSecondUser = await _unitOfWork.Chat.FindAllAsync(c => c.SenderId == secoundUserId && c.ReciveId == firstUser);
            var chatList = chatForFirstUser.ToList();
            for(int i =0;i< chatList.Count;i++)
                chatList[i].Read=true;
            _unitOfWork.Chat.UpdateRange(chatList);
            chatList= chatForSecondUser.ToList();
            for (int i = 0; i < chatList.Count; i++)
                chatList[i].Read = true;
            _unitOfWork.Chat.UpdateRange(chatList);
            return await _unitOfWork.Complete()>0;
        }

        public async Task<bool> SendPrivateMessage(ChatDTO chatDTO)
        {
            await _unitOfWork.Chat.AddAsync(new DataBase.Core.Models.Chat
            {
                Id = Guid.NewGuid(),
                Message = chatDTO.Message,
                PhotoPath = chatDTO.PhotoPath,
                ReciveId = chatDTO.ReciveId,
                SenderId = chatDTO.SenderId,
                VedioPath = chatDTO.VedioPath,
            });
            return await _unitOfWork.Complete() >0;
        }
        public async Task<IEnumerable<friendChat>> GetFriendsWithLastMessage(Guid userId)
        {
            var userFriends = await _acountService.GetAllUserFreinds(userId);
            var userChat = await _unitOfWork.Chat.FindAllAsync(c=>c.SenderId == userId);
            var userRevicedChat = await _unitOfWork.Chat.FindAllAsync(c => c.ReciveId == userId);

            var join1 = from friend in userFriends
                        join chatR in userChat
                        on friend.Id equals chatR.ReciveId
                        orderby chatR.TimeStamp descending
                        select new friendChat
                        {
                            UserId = friend.Id,
                            UserName = friend.UserName,
                            FirstName = friend.FirstName,
                            LastName = friend.LastName,
                            Email = friend.Email,
                            Message = chatR.Message,
                            PhotoPath = chatR.PhotoPath,
                            Read = chatR.Read,
                            TimeStamp = chatR.TimeStamp,
                            VedioPath= chatR.VedioPath,
                            ReciveId = chatR.ReciveId,
                            SenderId = chatR.SenderId,
                        };
            var join2 = from friend in userFriends
                        join chatS in userRevicedChat
                        on friend.Id equals chatS.SenderId
                        orderby chatS.TimeStamp descending
                        select new friendChat
                        {
                            UserId = friend.Id,
                            UserName = friend.UserName,
                            FirstName = friend.FirstName,
                            LastName = friend.LastName,
                            Email = friend.Email,
                            Message = chatS.Message,
                            PhotoPath = chatS.PhotoPath,
                            Read = chatS.Read,
                            TimeStamp = chatS.TimeStamp,
                            VedioPath = chatS.VedioPath,
                            ReciveId = chatS.ReciveId,
                            SenderId = chatS.SenderId,

                        };
            var AllJoin = join2.ToList();
            AllJoin.AddRange(join1.ToList());
            var finalResult = AllJoin.OrderByDescending(x => x.TimeStamp).DistinctBy(x=>x.UserId).Select( f=> new friendChat
            {
                Online=IsOnline(f.UserId),
                UserId = f.UserId,
                UserName = f.UserName,
                FirstName = f.FirstName,
                LastName = f.LastName,
                Email = f.Email,
                Message = f.Message,
                PhotoPath = f.PhotoPath,
                Read = f.Read,
                TimeStamp = f.TimeStamp,
                VedioPath = f.VedioPath,
                ReciveId = f.ReciveId,
                SenderId = f.SenderId,
            });
            return finalResult;
        }
        private bool IsOnline(Guid userId)
        {
            if (!string.IsNullOrEmpty(_userConnectionManager.GetUserConnection(userId)))
                return true;
            return false;
        }

            
        /// <summary>
        /// use this if meet performance Issue
        /// </summary>
        //public async Task<IEnumerable<friendChat>> GetFriendsWithLastMessageGPT(Guid userId)
        //{
        //    var userFriends = await _acountService.GetAllUserFreinds(userId);

        //    var userChatMessages = await _DbContext.Chat
        //        .Where(c => c.SenderId == userId || c.ReciveId == userId)
        //        .OrderByDescending(c => c.TimeStamp)
        //        .GroupBy(c => c.SenderId == userId ? c.ReciveId : c.SenderId)
        //        .Select(group => group.First()) // Select the latest message for each user
        //        .ToListAsync();

        //    var finalResult = userFriends
        //        .Join(userChatMessages,
        //            friend => friend.Id,
        //            chat => chat.SenderId == userId ? chat.ReciveId : chat.SenderId,
        //            (friend, chat) => new friendChat
        //            {
        //                UserId = friend.Id,
        //                UserName = friend.UserName,
        //                FirstName = friend.FirstName,
        //                LastName = friend.LastName,
        //                Email = friend.Email,
        //                Message = chat.Message,
        //                PhotoPath = chat.PhotoPath,
        //                Read = chat.Read,
        //                TimeStamp = chat.TimeStamp,
        //                VedioPath = chat.VedioPath,
        //                ReciveId = chat.ReciveId,
        //                SenderId = chat.SenderId,
        //            })
        //        .OrderByDescending(x => x.TimeStamp)
        //        .DistinctBy(x => x.UserId);

        //    return finalResult;
        //}

        public record friendChat
        {
            public Guid UserId { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Guid SenderId { get; set; }
            public Guid ReciveId { get; set; }
            public string? Message { get; set; }
            public string? PhotoPath { get; set; }
            public string? VedioPath { get; set; }
            public bool Read { get; set; }
            public bool Online { get; set; }
            public DateTime? TimeStamp { get; set; }
        }
    }
}
