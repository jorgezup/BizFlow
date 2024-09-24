using Core.Enums;

namespace Application.DTOs.Order;

public record OrderUpdateStatusRequest(
    Status Status
);