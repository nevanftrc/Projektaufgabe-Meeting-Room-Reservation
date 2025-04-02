    using AutoMapper;
    using RoomRevAPI.Models;

    namespace RoomRevAPI.Mapping
    {
        public class MapProfile : Profile
        {
            public MapProfile()
            {
                // map
                CreateMap<MeetingRooms, MeetingRoomDTO>().ReverseMap();
                CreateMap<Reservations, ReservationDTO>().ReverseMap();
                CreateMap<Reservers, ReserverDTO>().ReverseMap();
                CreateMap<Tools, ToolDTO>().ReverseMap();
        }
        }
    }

