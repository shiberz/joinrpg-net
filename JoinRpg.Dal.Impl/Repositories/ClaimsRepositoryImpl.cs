﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using JoinRpg.Data.Interfaces;
using JoinRpg.DataModel;
using System.Data.Entity;

namespace JoinRpg.Dal.Impl.Repositories
{
  [UsedImplicitly]
  public class ClaimsRepositoryImpl : GameRepositoryImplBase, IClaimsRepository
  {
    public ClaimsRepositoryImpl(MyDbContext ctx) : base(ctx)
    {
    }

    public Task<ICollection<Claim>> GetClaims(int projectId) => GetClaimsImpl_(projectId);

    public async Task<IEnumerable<Claim>> GetMyClaimsForProject(int userId, int projectId)
      => await Ctx.ClaimSet.Where(c => c.ProjectId == projectId && c.PlayerUserId == userId).ToListAsync();

    public async Task<IEnumerable<Claim>> GetClaimsByIds(int projectid, ICollection<int> claimindexes)
    {
      return
        await Ctx.ClaimSet.Include(c => c.Project.ProjectAcls.Select(pa => pa.User))
          .Include(c => c.Player)
          .Where(c => claimindexes.Contains(c.ClaimId))
          .ToListAsync();
    }

    private async Task<ICollection<Claim>> GetClaimsImpl_(int projectId)
    {
      await LoadProjectCharactersAndGroups(projectId);
      await LoadMasters(projectId);

      return await
        Ctx.ClaimSet    
          .Include(c => c.Comments.Select(cm => cm.Finance))
          .Include(c => c.Watermarks)
          .Include(c => c.Player)
          .Include(c => c.FinanceOperations)
          .Where(c => c.ProjectId == projectId).ToListAsync();
    }

    public async Task<ICollection<Claim>> GetActiveClaimsForMaster(int projectId, int userId)
      => (await GetClaimsImpl_(projectId)).Where(c => c.ResponsibleMasterUserId == userId).ToList();
  }
}
